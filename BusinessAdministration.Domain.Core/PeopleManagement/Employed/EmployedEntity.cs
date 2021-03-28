using BusinessAdministration.Domain.Core.PeopleManagement.Area;
using BusinessAdministration.Domain.Core.PeopleManagement.DocumentType;
using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessAdministration.Domain.Core.PeopleManagement.Employed
{
    public enum EmployedPosition
    {
        Manager = 1,
        Developer =2,
        Analyst =3,
        Testesr = 4
    }
    public enum EmployedCode
    {
        //TODO: Michael, changes this enum because is not unic code for each employed
        Mg= 1,
        Dv=2,
        Anl=3,
        Tst=4
    }
    public class EmployedEntity : PersonBase
    {
        [Key]
        [StringLength(30)]
        public Guid EmployedId { get; set; }
        [Required]
        public EmployedCode EmployedCode { get; set; }
        [Required]
        public override PersonType PersonType => PersonType.NaturalPerson;
        [Required]
        public EmployedPosition EmployedPosition { get; set; }
        [Required]
        [StringLength(30)]
        public Guid AreaId { get; set; }
        public virtual AreaEntity Area { get; set; }
        [Required]
        [StringLength(30)]
        public Guid DocumentTypeId { get; set; }
        public virtual DocumentTypeEntity DocumentType{ get; set; }
    }
}
