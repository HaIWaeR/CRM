using Domain.Enums;

namespace Application.DTO
{
    public class BranchToCreateOrUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public BranchStatus Status { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? Description { get; set; }
    }
}