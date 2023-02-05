using Queo.Commons.Builders.Model.Builder;

namespace Queo.Commons.Builders.Model.Examples.Car.Builders
{
    /// <summary>
    ///     A builder doesn't necessarly need to implement the IRecreatable interface
    ///     The IRecreatable is a use case for ModelBuilders because they don't allow modification after beeing build
    ///     So if you want to use this builder within a ModelBuilder you shold implement the IRecreatable interface
    /// </summary>
    public class NonRecreatableBuilder : IBuilder<Car>
    {
        public Car Build()
        {
            return new Car { Name = "Not a model car" };
        }
    }
}
