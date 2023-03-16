using FluentValidation.Results;


namespace Solforb.Orders.Application.Exceptions
{
	public class ValidationException : ApplicationException
	{ 
		public List<string> Errors { get; set; } = new List<string> ();

		public ValidationException(ValidationResult validationResult)
		{
			validationResult.Errors.ForEach(error =>
			{
				Errors.Add(error.ErrorMessage);
			});
		}
	}
}
