using System.Collections.Generic;
using System.Text;
using System;

namespace Coding.Exercise
{
  public class CodeBuilder
  {
    private class ClassField
    {
      public string Type;
      public string Name;
      public ClassField(string name, string type)
      {
        this.Name = name;
        this.Type = type;
      }
    }

    private readonly string className;
    private readonly List<ClassField> classFields = new List<ClassField>();

    public CodeBuilder(string className) =>
        this.className = className;

    public CodeBuilder AddField(string name, string type)
    {
      classFields.Add(new ClassField(name, type));
      return this;
    }

    public override string ToString() =>
        new StringBuilder()
            .Append("public class ")
            .AppendLine(className)
            .AppendLine("{")
            .AppendJoin("", classFields.ConvertAll(f => $"  public {f.Type} {f.Name};\n"))
            .AppendLine("}")
            .ToString();
  }

  class Program
  {
    static void Main(string[] args)
    {
      var cb = new CodeBuilder("Person")
        .AddField("Name", "string")
        .AddField("Age", "int");
      Console.WriteLine(cb);
    }
  }
}