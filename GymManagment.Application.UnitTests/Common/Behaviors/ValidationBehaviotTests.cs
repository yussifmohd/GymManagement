using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GymManagement.Application.Common.Behavior;
using GymManagement.Application.Gyms.Command.CreateGym;
using GymManagement.Domain.Gyms;
using MediatR;
using NSubstitute;
using TestsCommon.Gyms;

namespace GymManagement.Application.UnitTests.Common.Behaviors
{
    public class ValidationBehavotTests
    {
        private readonly ValidationBehavior<CreateGymCommand, ErrorOr<Gym>> _validationBehavior;
        private readonly IValidator<CreateGymCommand> _mockValidator;
        private readonly RequestHandlerDelegate<ErrorOr<Gym>> _mockNextBehavior;

        public ValidationBehavotTests()
        {
            //Create next behavior(mock)
            _mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<Gym>>>();

            //Create validator (mock)
            _mockValidator = Substitute.For<IValidator<CreateGymCommand>>();

            //Create validation behavior (System under test)
            _validationBehavior = new ValidationBehavior<CreateGymCommand, ErrorOr<Gym>>(_mockValidator);
        }

        [Fact]
        public async Task InvokeBehavior_WhenValidatorResultIsValid_ShouldInvokeNextBehavior()
        {
            //Arrange
            var createGymRequest = GymCommandFactory.CreateCreateGymCommand();
            var gym = GymFactory.CreateGym();

            _mockValidator
               .ValidateAsync(createGymRequest, Arg.Any<CancellationToken>())
               .Returns(new ValidationResult());

            _mockNextBehavior.Invoke().Returns(gym);

            //Act
            //Invoke Behavior
            var result = await _validationBehavior.Handle(createGymRequest, _mockNextBehavior, default);

            //Assert
            //Result from invoking the behavior, was the result returned by next behavior
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo(gym);
        
        }

        [Fact]
        public async Task InvokeBehavior_WhenValidatorResultIsNotValid_ShouldReturnListOfErrors()
        {
            //Arrange
            var createGymRequest = GymCommandFactory.CreateCreateGymCommand();
            List<ValidationFailure> validationFailures = [new(propertyName: "prop", errorMessage: "failed prop")];

            _mockValidator
               .ValidateAsync(createGymRequest, Arg.Any<CancellationToken>())
               .Returns(new ValidationResult(validationFailures));

            //Act
            //Invoke Behavior
            var result = await _validationBehavior.Handle(createGymRequest, _mockNextBehavior, default);

            //Assert
            //Result from invoking the behavior, was the result returned by next behavior
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be("prop");
            result.FirstError.Description.Should().Be("failed prop");

        }

    }
}
