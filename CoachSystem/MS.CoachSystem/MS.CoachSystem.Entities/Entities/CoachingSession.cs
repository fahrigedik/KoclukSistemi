﻿using MS.CoachSystem.Entity.Base;

namespace MS.CoachSystem.Entity.Entities;

public class CoachingSession : BaseEntity, IHasCreationAndModificationDates
{
    public string CoachId { get; set; } // AuthServer'daki Users tablosuna foreign key
    public string StudentId { get; set; } // AuthServer'daki Users tablosuna foreign key
    public DateTime SessionDate { get; set; }
    public TimeSpan SessionTime { get; set; } // TimeSpan, saati temsil etmek için daha uygun
    public string SessionTopic { get; set; } // Opsiyonel
    public string SessionNotes { get; set; } // Opsiyonel
    public string SessionLocation { get; set; } // Opsiyonel
    public string SessionStatus { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}