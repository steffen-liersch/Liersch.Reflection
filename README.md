[![NuGet](https://img.shields.io/nuget/v/Liersch.Reflection.svg)](https://www.nuget.org/packages/Liersch.Reflection)

# Liersch.Reflection

With the Reflection API, .NET offers an easy way to access types and their elements at runtime. Typical use cases are the serialization and deserialization of objects. During serialization, all relevant fields and properties of an object are transformed into a storable form such as JSON or XML with the help of the Reflection API. The deserialization restores an object from this storable form.

The late binding of types and elements at runtime causes lower performance. Operations that are normally performed by the compiler must be done at runtime when using the Reflection API.

`Liersch.Reflection` significantly improves the performance of the Reflection API by generating dynamic IL code for constructor and function calls as well as for field access. The access speed is almost like direct access. All major .NET platforms are supported (from .NET Framework 2.0, from .NET Core 2.0 and from .NET Standard 2.1).

The quality of the library is ensured by automated module tests. There are module tests available for relevant functions. All major library changes are logged in the [CHANGELOG.md](https://github.com/steffen-liersch/Liersch.Reflection/blob/main/CHANGELOG.md) file.

## Features

`Liersch.Reflection` is particularly suitable for the serialization and deserialization of objects and provides the following features:

- fast creation of classes using the standard constructor
- fast creation of value types
- fast call to functions
- fast reading and writing of properties
- fast reading and writing of fields

The library is written in C # 6.0. The file size of the compiled library is only ≈20 kB.

## Example

The demo application uses [Liersch.Profiling](https://github.com/steffen-liersch/Liersch.Profiling) to compare the performance of different scenarios. The measurement results show the speed advantage that can be achieved with `Liersch.Reflection`. The performance measurement should be performed outside the development environment and based on the release version. Otherwise, the additional effort for debugging will affect the measurement results.

## Integration

`Liersch.Reflection` contains the `Accelerator` class for generating function pointers for the fast creation of types, the fast calling of functions and the fast reading and writing of properties and fields.

Instead of creating objects with `Activator.CreateInstance`, the function pointer returned by `Accelerator.CreateStandardConstructor` must be used to create a new instances.

```cs
Func0 create=Accelerator.CreateStandardConstructor(type);
// ...
object inst=create();
```

Furthermore, some selected functions of the `Accelerator` class are also available as extension functions with a simplified signature.

```cs
InvocationDelegate toLower=typeof(string).CreateInvocationDelegate("ToLowerInvariant");
// ...
object o=toLower("Hello World!");
```

For functions with up to two parameters, specialized functions are available that do not use an array for parameter transfer. If non-static functions are called, the first parameter always represents the instance to be used. To manipulate value types, they must first be boxed into the heap memory (e.g. by type conversion to `object`). This is the only way to ensure that the manipulation functions always process the same object and not a new copy with every call. There is no support for parameters passed by reference (see key words `ref` and `out`).

The performance of reflection-based accesses is improved on the basis of dynamically generated code. The effort of code generation is only worthwhile if a higher number of accesses is expected.

## License

The software is published under the conditions of an open source license. Alternatively, other terms can be agreed under a commercial license. You can support the maintenance and further development of the software with a [voluntary donation](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=NVXEQCNGJFK92).

## Copyright

Copyright © 2020 Steffen Liersch
https://www.steffen-liersch.de/

## Links

The source code is maintained on GitHub:
https://github.com/steffen-liersch/Liersch.Reflection

Packages can be downloaded through NuGet:
https://www.nuget.org/packages/Liersch.Reflection
