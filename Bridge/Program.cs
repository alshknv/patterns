using System;
using Autofac;

namespace Coding.Exercise
{
    public abstract class Shape
    {
        protected IRenderer renderer;
        public string Name { get; set; }

        public Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public override string ToString() => $"Drawing {Name} as {renderer.WhatToRenderAs}";
    }

    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs { get; } = "lines";
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs { get; } = "pixels";
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer) => Name = "Triangle";
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer) => Name = "Square";
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Triangle(new RasterRenderer()).ToString());

            // dependency injection
            var cb = new ContainerBuilder();
            cb.RegisterType<VectorRenderer>().As<IRenderer>(); // drawing using vector renderer always
            cb.RegisterType<Square>();

            using var container = cb.Build();

            Console.WriteLine(
                container.Resolve<Square>().ToString()
            );
        }
    }
}

