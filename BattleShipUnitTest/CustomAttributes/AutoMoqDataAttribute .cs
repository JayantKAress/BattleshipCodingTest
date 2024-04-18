
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace BattleshipUnitTest.CustomTestAttributes
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute(int depth = 5) : base(() =>
        {
            var fixture = new Fixture().Customize(new CompositeCustomization(new AutoMoqCustomization(), new SupportMutableValueTypesCustomization()));
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            fixture.Register<TimeSpan>(() => new TimeSpan(0, 0, 0));
            fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));

            return fixture;
        })
        { }
    }
}