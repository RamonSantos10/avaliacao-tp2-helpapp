using HelpApp.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace HelpApp.Domain.Test
{
    public class CategoryUnitTest
    {
        #region Testes Positivos

        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name");
            action.Should().NotThrow<HelpApp.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category With Only Name")]
        public void CreateCategory_OnlyName_ResultObjectValidState()
        {
            Action action = () => new Category("Only Name");
            action.Should().NotThrow<HelpApp.Domain.Validation.DomainExceptionValidation>();
        }

        #endregion

        #region Testes Negativos

        [Theory(DisplayName = "Create Category With Invalid Names")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("AB")]
        public void CreateCategory_InvalidNames_ThrowsDomainException(string invalidName)
        {
            Action action = () => new Category(1, invalidName);

            if (string.IsNullOrEmpty(invalidName))
            {
                action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                    .WithMessage("Invalid name, name is required.");
            }
            else
            {
                action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                    .WithMessage("Invalid name, too short, minimum 3 characters.");
            }
        }

        [Fact(DisplayName = "Create Category With Negative Id")]
        public void CreateCategory_WithNegativeId_ResultObjectException()
        {
            Action action = () => new Category(-1, "Valid Name");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value.");
        }

        #endregion
    }
}
