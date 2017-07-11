using BinaryFormatter;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BinaryFormatterTests
{
    public class WhenWorkingWith_Classes
    {
        class WithoutCtor
        {
            public int Int { get; set; }
            public double Double { get; set; }
            public string String { get; set; }
        }

        class WithCtor
        {
            public WithCtor(int i, double d, string s)
            {
                Int = i;
                Double = d;
                String = s;
            }

            public int Int { get; set; }
            public double Double { get; set; }
            public string String { get; set; }
        }

        abstract class WithVirtualProperties
        {
            protected static List<WithVirtualProperties_Element> _elements = new List<WithVirtualProperties_Element>();
            public static IReadOnlyCollection<WithVirtualProperties_Element> AllElements
            {
                get
                {
                    return _elements as IReadOnlyCollection<WithVirtualProperties_Element>;
                }
            }
            public static IReadOnlyCollection<WithVirtualProperties_Group> AllGroups
            {
                get
                {
                    return _elements.Select(gr => gr.Group)
                        .Distinct()
                        .ToList() as IReadOnlyCollection<WithVirtualProperties_Group>;
                }
            }

            public string Name { get; set; }

            public WithVirtualProperties()
            {
            }
            public WithVirtualProperties(string Name)
            {
                this.Name = Name;
            }

            public override string ToString()
            {
                return this.Name;
            }
        }

        class WithVirtualProperties_Element : WithVirtualProperties, IDisposable
        {
            public WithVirtualProperties_Group Group { get; set; }

            public WithVirtualProperties_Element()
            {

            }
            public WithVirtualProperties_Element(string Name, WithVirtualProperties_Group Group):base(Name)
            {
                this.Group = Group;
                _elements.Add(this);
            }

            public virtual IEnumerable<WithVirtualProperties_Group> Groups {
                get
                {
                    return WithVirtualProperties.AllElements
                        .Where(el => el == this)
                        .Select(gr => gr.Group)
                        .Distinct()
                        .ToList() as IEnumerable<WithVirtualProperties_Group>;
                }
            }

            public void Dispose()
            {
                _elements.Remove(this);
            }
        }

        class WithVirtualProperties_Group : WithVirtualProperties
        {
            public WithVirtualProperties_Group()
            {

            }
            public WithVirtualProperties_Group(string Name) : base(Name)
            {
            }

            public virtual IEnumerable<WithVirtualProperties_Element> Elements {
                get {

                    return WithVirtualProperties.AllElements
                        .Where(el => el.Group == this)
                        .Select(el => el)
                        .ToList() as IEnumerable<WithVirtualProperties_Element>;
                }
            }
        }

        [Fact]
        public void CanWorkWith_ClassesWithoutCustomCtor_WithProperties_WithPublicSetter()
        {
            var before = new WithoutCtor(){Int = 1, Double = 1, String = "lorem ipsum"};

            var formatter = new BinaryConverter();
            byte[] data = formatter.Serialize(before);

            var after = formatter.Deserialize<WithoutCtor>(data);

            Assert.Equal(before.String, after.String);
            Assert.Equal(before.Int, after.Int);
            Assert.Equal(before.Double, after.Double);
        }

        [Fact(Skip = "Work in progress")]
        public void CanWorkWith_ClasseWithCustomCtor_WithProperties_WithPublicSetter()
        {
            var before = new WithCtor(1, 1, "lorem ipsum");

            var formatter = new BinaryConverter();
            byte[] data = formatter.Serialize(before);

            var after = formatter.Deserialize<WithCtor>(data);

            Assert.Equal(before.String, after.String);
            Assert.Equal(before.Int, after.Int);
            Assert.Equal(before.Double, after.Double);
        }

        [Fact]
        public void CanWorkWith_ClassesWithoutCustomCtor_WithVirtualProperties_WithPublicSetter()
        {
            for (int iGroup = 1; iGroup <= 5; iGroup++)
            {
                string groupName = string.Format("Group {0}", iGroup);
                WithVirtualProperties_Group currentGroup = new WithVirtualProperties_Group(groupName);

                for (int iElement = 1; iElement <= 5; iElement++)
                {
                    string nameElement = string.Format("Element {0}-{1}", iGroup, iElement);
                    WithVirtualProperties_Element currentElement = new WithVirtualProperties_Element(nameElement, currentGroup);
                }
            }

            var formatter = new BinaryConverter();

            foreach (WithVirtualProperties_Group group in WithVirtualProperties.AllGroups)
            {
                byte[] data = formatter.Serialize(group);
                WithVirtualProperties_Group after = formatter.Deserialize<WithVirtualProperties_Group>(data);

                Assert.Equal(group.Name, after.Name);
            }
        }
    }
}
