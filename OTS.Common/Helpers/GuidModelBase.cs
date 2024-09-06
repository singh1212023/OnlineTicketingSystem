using System.ComponentModel.DataAnnotations;

namespace OTS.Common
{
    public abstract class GuidModelBase
    {
        [StringLength(36)]
        [Key]
        public string Id { get; set; }
        protected GuidModelBase()
        {
            Id = Guid.NewGuid().ToString();
        }

    }

}