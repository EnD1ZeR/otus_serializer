using Newtonsoft.Json;
using Serializer.Abstractions;
using Serializer.Csv;
using Serializer.Csv.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Reflection
{
    class Sample
    {
        private int _field;

        public int Field { get => _field; }

        public Sample()
        {
            _field = 42;
        }

        public Sample(int field)
        {
            _field = field;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //FieldMagic();
            //PropertiesMagic();
            //DynamicAssembly();
            //DynamicSample();
            //Test();
            Test2();
        }

        public class G
        {
            int iG1 { get; set; }
            string stringG2 { get; set; }
            //public MyF fClass;

            public G GetG() => new G()
            {
                iG1 = 632,
                stringG2 = "GClassIsTheBest"
            };

            public override string ToString()
            {
                return $"[G]:\n{iG1}\n{stringG2}";
            }
        }

        public class MyF
        {
            [NonSerialized]
            int i1;
            int i2, i3, i4, i5;
            string string1;
            Guid guid1;
            double double1;
            int i6;
            G gClass1;

            public MyF Get()
            {
                var fClass = new MyF()
                {
                    i1 = 1,
                    i2 = 2,
                    i3 = 3,
                    i4 = 4,
                    i5 = 5,
                    string1 = "mystrin\"g",
                    guid1 = Guid.NewGuid(),
                    double1 = 1.23,
                    i6 = 6,
                    gClass1 = new G().GetG()
                };
                //fClass.gClass1.fClass = fClass;

                return fClass;
            }

            public override string ToString()
            {
                return $"\n[F]\n{i1}\n{i2}\n{i3}\n{i4}\n{i5}\n{string1}\n{guid1}\n{double1}\n{i6}\n{gClass1}";
            }
        }

        public class F
        {
            int i1, i2, i3, i4, i5;

            public F Get() => new F()
            {
                i1 = 1,
                i2 = 2,
                i3 = 3,
                i4 = 4,
                i5 = 5
            };
        }

        // Тестирование проверки на циклическую зависимость, вложенность и проверку атрибута NonSerialized.
        // На данный момент поддержка возможности выбрать вложенность убрана, достаточно проверки на циклическую зависимость.
        static void Test()
        {
            var fInstance = new MyF().Get();

            Console.WriteLine(fInstance.ToString());
            
            var serializer = new CsvSerializer(new CsvSerializerOptions() { Delimeter = "<del>"});
            var serialized = serializer.SerializeFromObject(fInstance);
            var deserialized = serializer.DeserializeToObject<MyF>(serialized);
            Console.WriteLine(deserialized.ToString());
        }

        // Мейн задание
        static void Test2()
        {
            const int iterationsCount = 10000;

            var fClass = new F().Get();

            var serializer = new CsvSerializer(new CsvSerializerOptions() { Delimeter = "<d>" });
            var serializedByMySerializer = serializer.SerializeFromObject(fClass);
            var serializedByMicrosoftJson = System.Text.Json.JsonSerializer.Serialize(fClass);
            var serializedByNewtonsoftJson = JsonConvert.SerializeObject(fClass);

            // MySerializer - Serialize
            RunTest(() => 
            {
                serializer = new CsvSerializer(new CsvSerializerOptions() { Delimeter = "<d>" });
                var serialized = serializer.SerializeFromObject(fClass);
            },
            "My serializer - serialize", iterationsCount);

            // MySerializer - Deserialize
            RunTest(() =>
            {
                serializer = new CsvSerializer(new CsvSerializerOptions() { Delimeter = "<d>" });
                var deserialized = serializer.DeserializeToObject<F>(serializedByMySerializer);
            },
            "My serializer - deserialize", iterationsCount);

            
            // MicrosoftJson - Serialize
            RunTest(() =>
            {
                var serialized = System.Text.Json.JsonSerializer.Serialize(fClass);
            },
            "MicrosoftJson - serialize", iterationsCount);

            // MicrosoftJson - Deserialize
            RunTest(() =>
            {
                var f = System.Text.Json.JsonSerializer.Deserialize<F>(serializedByMicrosoftJson);
            },
            "MicrosoftJson - deserialize", iterationsCount);

            // MicrosoftJson - Serialize
            RunTest(() =>
            {
                var serialized = System.Text.Json.JsonSerializer.Serialize(fClass);
            },
            "NewtonsoftJson - serialize", iterationsCount);

            // MicrosoftJson - Deserialize
            RunTest(() =>
            {
                var f = System.Text.Json.JsonSerializer.Deserialize<F>(serializedByMicrosoftJson);
            },
            "NewtonsoftJson - deserialize", iterationsCount);

            Console.ReadKey();
        }

        static void RunTest(Action func, string testName, int iterations)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                func.Invoke();
            }

            stopWatch.Stop();
            Console.WriteLine($"{testName} (ms): {stopWatch.ElapsedMilliseconds}, Iterations: {iterations}");
        }

        static void FieldMagic()
        {
            Sample sample = new Sample();

            Type sampleType = sample.GetType();
            FieldInfo fieldInfo = sampleType
                .GetField(name: "field", bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic);

            //get value
            object fieldValue = fieldInfo.GetValue(obj: sample);
            Console.WriteLine(value: fieldValue);

            //set value
            fieldInfo.SetValue(obj: sample, value: 1);
            int sampleField = sample.Field;
            Console.WriteLine(value: sampleField);
        }

        static void PropertiesMagic()
        {
            Type type = typeof(string);
            PropertyInfo[] props = type.GetProperties();
            Console.WriteLine(value: "Properties:");
            foreach (PropertyInfo prop in props)
            {
                Console.WriteLine(value: prop.Name);
            }
            Console.WriteLine();

            FieldInfo[] fieldInfos = type.GetFields();
            Console.WriteLine(value: "Fields:");
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                Console.WriteLine(value: fieldInfo.Name);
            }
            Console.WriteLine();

            MethodInfo[] methodInfos = type.GetMethods();
            Console.WriteLine(value: "Methods:");
            foreach (MethodInfo methodInfo in methodInfos)
            {
                Console.WriteLine(value: methodInfo.Name);
            }

            //https://stackoverflow.com/questions/41468722/loop-reflect-through-all-properties-in-all-ef-models-to-set-column-type

            //https://stackoverflow.com/questions/19792295/mapping-composite-keys-using-ef-code-first
        }

        static void DynamicAssembly()
        {
            string solutionRoot = Directory.GetParent(path: Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string assemblyFile = Path.Combine(solutionRoot, @"MyLibrary\bin\Debug\netcoreapp3.1\MyLibrary.dll");
            if (!File.Exists(assemblyFile))
            {
                throw new FileNotFoundException("please build MyLibrary project");
            }

            Assembly assembly = Assembly.LoadFrom(assemblyFile: assemblyFile);
            Console.WriteLine(value: assembly.FullName);
            // получаем все типы из сборки MyLibrary.dll
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                Console.WriteLine(value: type.Name);
            }

            // Позднее связывание
            Type myLibraryClassType = assembly.GetType(name: "MyLibrary.MyLibraryClass", throwOnError: true, ignoreCase: true);
            
            // создаем экземпляр класса
            object myLibraryClass = Activator.CreateInstance(type: myLibraryClassType);

            MethodInfo sumMethodInfo = myLibraryClassType.GetMethod(name: "Sum");

            object result = sumMethodInfo.Invoke(obj: myLibraryClass, parameters: new object[] { 2, 6 });

            Console.WriteLine(value: result);
        }

        static void DynamicSample()
        {
            dynamic sample1 = new Sample();
            var sample2 = new Sample();
            object sample3 = new Sample();
            Sample sample4 = new Sample();

            Console.WriteLine(sample1.Field);
            //Console.WriteLine(sample1.Method());
            Console.WriteLine();

            Console.WriteLine(value: "dynamic: " + sample1.GetType().Name); 
            Console.WriteLine(value: "var: " + sample2.GetType().Name);
            Console.WriteLine(value: "object: " + sample3.GetType().Name); 
            Console.WriteLine(value: "Sample: " + sample4.GetType().Name);
            Console.WriteLine();

            dynamic expando = new ExpandoObject();
            Console.WriteLine(value: "expando: " + expando.GetType().Name);

            expando.Name = "Brian";
            expando.Country = "USA";
            expando.City = new object();

            expando.IsValid = (Func<int, bool>)((number) =>
            {
                // Check that they supplied a name
                return !string.IsNullOrWhiteSpace(value: expando.Name);
            });

            expando.Print = (Action)(() =>
            {
                Console.WriteLine(value: $"{expando.Name} {expando.Country} {expando.IsValid(456456)}");
            });

            expando.Print();
            expando.Name = "Jack";
            expando.Print();
        }
    }
}