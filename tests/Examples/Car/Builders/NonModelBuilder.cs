using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Examples.Car.Builders
{
    /// <summary>
    ///     You can create your own version of a builder by implementing the required interfaces
    /// </summary>
    public class NonModelBuilder : IBuilder<Car>, IRecreatable<NonModelBuilder>
    {
        public Car Build()
        {
            return new Car { Name = "Not a model car" };
        }

        public NonModelBuilder Recreate()
        {
            return new NonModelBuilder();
        }
    }
}
