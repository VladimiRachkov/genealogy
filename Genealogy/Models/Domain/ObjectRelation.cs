using System;

namespace Genealogy
{
    public class ObjectRelation
    {
        public Guid Id { get; set; }
        public Guid BusinessObjectId { get; set; }
        public Guid LinkRelationId { get; set; }
        public Guid MetatypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}