using FleetManager.Exception.ExceptionBase;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace FleetManager.Application.UseCase.ToUser
{
    public partial class PasswordValidator<T> : PropertyValidator<T, string>
    {
        private const string ERROR_MESSAGE_KEY = "ErrorMessage ";
        public override string Name => "PasswordValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"{{{ERROR_MESSAGE_KEY}}}";
        }

        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if(string.IsNullOrWhiteSpace(password))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }

            if(password.Length < 8)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }
            if(!UpperCaseLetters().IsMatch(password))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_MUST_CONTAIN_AN_UPPERCASE_LETTER);
                return false;
            }
            if(!LowerCaseLetters().IsMatch(password))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_MUST_CONTAIN_AN_LOWRECASE_LETTER);
                return false;
            }
            if(!Numbers().IsMatch(password))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_MUST_CONTAIN_NUMBER);
                return false;
            }

            return true;
        }



        [GeneratedRegex(@"[a-z]+")]
        private static partial Regex LowerCaseLetters();
        [GeneratedRegex(@"[0-9]+")]
        private static partial Regex Numbers();
        [GeneratedRegex(@"[A-Z]+")]
        private static partial Regex UpperCaseLetters();

    }
}
