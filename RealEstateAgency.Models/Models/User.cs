namespace RealEstateAgency.Models
{
    using System.Collections.Generic;
    public partial class User
    {
        public User()
        {
            CallHistories = new List<CallHistory>();
            People = new List<Person>();
            People1 = new List<Person>();
            PersonOtherInfoes = new List<PersonOtherInfo>();
            PageListPermissions = new List<PageListPermission>();
            Attachments = new List<Attachment>();
            Attachments_Delete = new List<Attachment>();
            CallRefersBy = new List<CallRefer>();
            CallRefersTo = new List<CallRefer>();
            CreateFile = new List<EstateFile>();
            UpdateFile = new List<EstateFile>();
            Events_CreateUser = new List<PersonEvents>();
            Events_UpdateUser = new List<PersonEvents>();
            Events_ConsultantUser = new List<PersonEvents>();
            FileRefers = new List<FileRefer>();
            FileRefers_ReferTo = new List<FileRefer>();
            AuthTokens = new List<AuthToken>();
            FileRequests = new List<FileRequest>();
            Files_ConsultantUser = new List<EstateFile>();
            FollowUpEvents = new List<FollowUpEvents>();
        }

        public int UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<CallHistory> CallHistories { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Person> People1 { get; set; }
        public virtual ICollection<PersonOtherInfo> PersonOtherInfoes { get; set; }
        public virtual ICollection<PageListPermission> PageListPermissions { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Attachment> Attachments_Delete { get; set; }
        public virtual ICollection<CallRefer> CallRefersBy { get; set; }
        public virtual ICollection<CallRefer> CallRefersTo { get; set; }
        public virtual ICollection<EstateFile> CreateFile { get; set; }
        public virtual ICollection<EstateFile> UpdateFile { get; set; }
        public virtual ICollection<EstateFile> Files_ConsultantUser { get; set; }
        public virtual ICollection<PersonEvents> Events_CreateUser { get; set; }
        public virtual ICollection<PersonEvents> Events_UpdateUser { get; set; }
        public virtual ICollection<PersonEvents> Events_ConsultantUser { get; set; }
        public virtual ICollection<FileRefer> FileRefers { get; set; }
        public virtual ICollection<FileRefer> FileRefers_ReferTo { get; set; }
        public virtual ICollection<AuthToken> AuthTokens { get; set; }
        public virtual ICollection<FileRequest> FileRequests { get; set; }
        public virtual ICollection<FollowUpEvents> FollowUpEvents { get; set; }
    }
}
