using System;

using Queo.Commons.Builders.Model.Builder;
using Queo.Commons.Builders.Model.Factory;

namespace Queo.Commons.Builders.Model.Examples.Car.Builders
{
    public class WheelBuilder : ModelBuilder<Wheel>
    {
        private string _brand;
        private int _position;

        public WheelBuilder(IBuilderFactory factory) : base(factory)
        {
            _brand = $"Brand {BuilderIndex}";
            _position = 1;
        }

        public WheelBuilder WithBrand(string name) => Set(() => _brand = name);
        public WheelBuilder WithPosition(int position) => Set(() => _position = position);


        protected override Wheel BuildModel()
        {
            return new Wheel { Brand = _brand, Position = _position };
        }

        protected override WheelBuilder Set(Action action) => Set<WheelBuilder>(action);
        public override WheelBuilder Recreate() => Recreate<WheelBuilder>();
        public static implicit operator Wheel(WheelBuilder builder) => builder.Build();
    }
}
