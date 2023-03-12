export class MemberDetailsModel {
        
    public UserId: string;
    public FullName: string;
    public Email: string;
    public Phone: string;

}

export class AddMemberModel {

    public FirstName: string;
    public LastName: string; 
    public Email: string; 
    public PhoneNumber: string; 
    public Address: string; 
    public City: string; 
    public State: string; 
    public Zip: string; 
    public Password: string; 
    
}

export class ChurchModel {

    public Name: string;
    public Email: string;
    public PhoneNumber: string;
    public Address: string;
    public City: string;
    public State: string;
    public Zip: string;
    public ChurchLogoDataUrl: string;
    public ChurchLogoMime: string;

}