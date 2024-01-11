using FluentValidation;

namespace RatioMusic.Application.ViewModels.Validations
{
    public class SongApiRequestValidation : AbstractValidator<SongApiRequest>
    {
        public SongApiRequestValidation() 
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x=>x.Name).MaximumLength(10);
        }
    }
}
