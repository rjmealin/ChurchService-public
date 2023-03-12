

export class UserModel {

    public UserId: string;
    public Email: string;
    public FirstName: string;
    public LastName: String;
    public Password: string;
    public AllowTextNotifications: boolean;

}
 

export class LoginModel {
    public Email: string;
    public Password: string;
}

export class PasswordResetModel {

    public PasswordResetId: string;
    public Password: string;

}

export class PasswordChangeModel {

    public NewPassword: string;
    public OldPassword: string;
    
}

export class AccountRecoverModel {

    public Email: string;
    public UsePhone: boolean;

}



