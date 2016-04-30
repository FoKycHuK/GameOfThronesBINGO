using GoTB.Domain.Entities;

namespace GoTB.WebUI.Infrastructure
{
    public static class CartHelpers
    {
        public static VoteType GetVoteTypeForCharacter(Character character, Cart cart)
        {
            return cart.CharacterIds.Contains(character.Id)
                ? VoteType.AlreadyVoted
                : cart.Points > character.Price
                    ? VoteType.CanVote
                    : VoteType.Disabled;
        }
    }
}