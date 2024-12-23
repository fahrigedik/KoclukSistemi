namespace MS.CoachSystem.Entity.Base;

public interface IHasCreationAndModificationDates
{
    DateTime CreationDate { get; set; }
    DateTime? ModificationDate { get; set; }
}

