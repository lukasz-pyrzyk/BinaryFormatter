using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFormatter
{
    public class Parser
    {
        public byte[] Parse(object obj)
        {
            Type t = obj.GetType();

            ICollection<PropertyInfo> properties = t.GetProperties().ToArray();
            int totalSize = 150 + sizeof(int) * properties.Count;

            byte[] array = new byte[totalSize];

            int offset = 0;
            foreach (PropertyInfo property in t.GetProperties())
            {
                object prop = property.GetValue(obj);
                Byte[] elementBytes = GetBytesFromEement(prop);

                Array.ConstrainedCopy(elementBytes, 0, array, offset, elementBytes.Length);
                offset += elementBytes.Length;
            }

            return new byte[0];
        }

        private static byte[] GetBytesFromEement(object element)
        {
            byte[] elementBytes = new byte[0];
            int elementSize = 0;
            if (element is short)
            {
                elementBytes = BitConverter.GetBytes((short) element);
                elementSize = sizeof (short);
            }
            if (element is ushort)
            {
                elementBytes = BitConverter.GetBytes((ushort)element);
                elementSize = sizeof(ushort);
            }
            else if (element is int)
            {
                elementBytes = BitConverter.GetBytes((int)element);
                elementSize = sizeof(int);
            }
            else if (element is uint)
            {
                elementBytes = BitConverter.GetBytes((uint)element);
                elementSize = sizeof(uint);
            }
            else if (element is long)
            {
                elementBytes = BitConverter.GetBytes((long)element);
                elementSize = sizeof(long);
            }
            else if (element is ulong)
            {
                elementBytes = BitConverter.GetBytes((ulong)element);
                elementSize = sizeof(ulong);
            }
            else if (element is float)
            {
                elementBytes = BitConverter.GetBytes((float)element);
                elementSize = sizeof(float);
            }
            else if (element is double)
            {
                elementBytes = BitConverter.GetBytes((double)element);
                elementSize = sizeof(double);
            }
            else if (element is char)
            {
                elementBytes = BitConverter.GetBytes((char)element);
                elementSize = sizeof(char);
            }
            else if (element is string)
            {
                string selement = (string) element;
                elementBytes = Encoding.UTF8.GetBytes(selement);
                elementSize = selement.Length;
            }
            else if (element is DateTime)
            {
                DateTime date = (DateTime) element;
                elementBytes = BitConverter.GetBytes(date.Ticks);
                elementSize = sizeof (long);
            }
            // TODO decimal

            byte[] sizeBytes = BitConverter.GetBytes(elementSize);
            Byte[] finalPackage = new byte[sizeBytes.Length + elementBytes.Length];
            
            Array.ConstrainedCopy(sizeBytes, 0, finalPackage, 0, sizeBytes.Length);
            Array.ConstrainedCopy(elementBytes, 0, finalPackage, sizeBytes.Length, elementBytes.Length);

            return finalPackage;
        }
    }
}
