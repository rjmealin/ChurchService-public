import { StringNullableChain } from "lodash";

export enum ReferralTypes {
    Email = 1,
    Flyer = 2,
    Website = 3,
    InviteFromMember = 4, 
    NewsPaper = 5,
    SocialMedia = 6
}

export class VisitorCardModel {
    
    public  VisitorCardId: string;
    public  ChurchId: string | null;  
    public  VisitorFirstName: string | null; 
    public  VisitorLastName: string | null; 
    public  VisitorEmail: string | null; 
    public  VisitorPhone: string | null; 
    public  VisitorAddress: string | null; 
    public  VisitorCity: string | null; 
    public  VisitorState: string | null; 
    public  VisitorZip: string | null; 
    public  ReferalType: ReferralTypes | null; 
    public  ReasonForAttendance: string | null; 
    public  Comments: string | null; 
    public  DateOfAttendance: Date | null;
    public  VisitorAge:  number | null;
    public  IsNewToArea:  boolean = false;
    public  IsExistingChristian:  boolean = false;
    public  IsReturningVisitor:  boolean = false;
    public  HomeChurch:  string | null;
    public  IsFirstTimeGuest:  boolean = false;
        
}

export class PlannedVisitModel {
        public PlannedVisitId: string;
        public ChurchId : string;
        public AssignedUserId: string | null;
        public AssignedMemberFirstName: string;
        public AssignedMemberLastName: string;
        public VisitDate: Date;
        public VisitorFirstName:string;
        public VisitorLastName:string; 
        public VisitorPhone:string; 
        public VisitorEmail:string;
        public CommentsOrQuestions: string;
}

export class DatabaseOptionModel {

    public DatabaseId: string;
    public Description: string;

}

export class DateModel {

    public Date: Date;

}

export class MemberProfileModel {
    
    public Email: string; 
    public Phone: string; 
    public FirstName: string; 
    public LastName: string; 
    public EnableTextNotifications: boolean; 
    public PhoneVerified: boolean;
    public ProfileImageDataUrl: string; 
    public ProfileImageMime: string;

}

export class EmailBlastModel {
    
    public CustomMessage: string; 
    public Emails: string[] = [];
    public EventDate: Date | null;
    public Subject: string;
}
