namespace RealEstateAgency.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Mapping;

    public partial class AgencyContext : DbContext
    {
        public AgencyContext()
            : base("name=AgencyContext")
        {
        }

        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<CallHistory> CallHistories { get; set; }
        public virtual DbSet<CallRefer> CallRefers { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<EstateFile> EstateFiles { get; set; }
        public virtual DbSet<PersonEvents> PersonEvents { get; set; }
        public virtual DbSet<FileGroup> FileGroups { get; set; }
        public virtual DbSet<FileGroupRelation> FileGroupRelations { get; set; }
        public virtual DbSet<FileRefer> FileRefers { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonGroup> PersonGroups { get; set; }
        public virtual DbSet<PersonGroupRelation> PersonGroupRelations { get; set; }
        public virtual DbSet<PersonOtherInfo> PersonOtherInfoes { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<PageList> PageLists { get; set; }
        public virtual DbSet<PageListPermission> PageListPermissions { get; set; }
        public virtual DbSet<AuthToken> AuthTokens { get; set; }
        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public virtual DbSet<EventDetail> EventDetails { get; set; }
        public virtual DbSet<FileRequest> FileRequests { get; set; }
        public virtual DbSet<FollowUpEvents> FollowUpEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new EventDetailMap());
            modelBuilder.Configurations.Add(new AttachmentMap());
            modelBuilder.Configurations.Add(new CallHistoryMap());
            modelBuilder.Configurations.Add(new CallReferMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new EstateFileMap());
            modelBuilder.Configurations.Add(new PersonEventsMap());
            modelBuilder.Configurations.Add(new FileGroupMap());
            modelBuilder.Configurations.Add(new FileGroupRelationMap());
            modelBuilder.Configurations.Add(new FileReferMap());
            modelBuilder.Configurations.Add(new PageListMap());
            modelBuilder.Configurations.Add(new PageListPermissionMap());
            modelBuilder.Configurations.Add(new PersonGroupMap());
            modelBuilder.Configurations.Add(new PersonGroupRelationMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new PersonOtherInfoMap());
            modelBuilder.Configurations.Add(new ProvinceMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new ZoneMap());
            modelBuilder.Configurations.Add(new AuthTokenMap());
            modelBuilder.Configurations.Add(new ExceptionLogMap());
            modelBuilder.Configurations.Add(new FileRequestMap());
            modelBuilder.Configurations.Add(new FollowUpEventsMap());

        }
    }
}
