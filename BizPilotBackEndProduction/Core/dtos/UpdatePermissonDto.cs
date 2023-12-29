using System.ComponentModel.DataAnnotations;

namespace BizPilotBackEndProduction.Core.dtos
{
    public class UpdatePermissonDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

    }
}
