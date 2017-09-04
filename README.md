# BinaryFormatter  [![NuGet version](https://badge.fury.io/nu/BinaryFormatter.svg)](https://badge.fury.io/nu/BinaryFormatter)
BinaryFormatter is a byte serialized/deserializer for .NET Core, created for Distributed Cache Platform - [Kronos][kronos-url]. After few days of development, [Protobuf-Net][protobuf-net-url] (contract base serializer, fork of Google Protobuf) has announcement  support for .NET Core. 

### Build status
| Windows |  Linux |
|:-------:|:------:|
|  [![Build status](https://ci.appveyor.com/api/projects/status/pklx6d4i71i8lbu4?svg=true)](https://ci.appveyor.com/project/LukaszPyrzyk/binaryformatter) | [![Build Status](https://travis-ci.org/lukasz-pyrzyk/BinaryFormatter.svg?branch=master)](https://travis-ci.org/lukasz-pyrzyk/BinaryFormatter) |



### Supported types
  - char
  - (s)byte
  - (u)short
  - (u)int
  - (u)long
  - float
  - double
  - bool
  - decimal
  - DateTime
  - DateTimeOffset
  - TimeSpan
  - byte[]
  - classes
  - collections
    - IEnumarable
    - IDictionary
    - LinkedList
  - structures
  - guid
  - uri
  - enum
  - KeyValuePair
  - BigInteger

 ### Not supported types
- anonymous types
- HashSet
- Array[,], Array[,,], Array[,,,] types
- Nullable

### Installation
```powershell
$ Install-Package BinaryFormatter
```

### Serialization
```cs
var converter = new BinaryConverter();
byte[] byteArray = converter.Serialize(model);
```    

### Deserialization
```cs
var converter = new BinaryConverter();
ViewModel obj = converter.Deserialize<ViewModel>(byteArray);
```    

## Note on Patches/Pull Requests

 * Fork the project.
 * Make your feature addition or bug fix.
 * Add tests for it. This is important so we don't break it in a future version unintentionally.
 * Send a pull request. Bonus points for topic branches.

License
----
MIT

   [kronos-url]: <https://github.com/lukasz-pyrzyk/Kronos>
   [protobuf-net-url]: <https://github.com/mgravell/protobuf-net>
