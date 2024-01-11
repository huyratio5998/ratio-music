using FluentValidation;

namespace RatioMusic.Application.ViewModels.Validations
{
    public class ArtistApiRequestValidation : AbstractValidator<ArtistApiRequest>
    {
        public ArtistApiRequestValidation() 
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);            
        }
    }
}
