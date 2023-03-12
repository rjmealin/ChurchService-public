

export class JsonWebTokenModel {

    public email: string | undefined = undefined;
    public userId: string | undefined = undefined;
    public churchId: string | undefined = undefined;
    public fullName: string | undefined = undefined;
    public expires: Date | undefined = undefined;
    public isAdmin: boolean = false;
    public emailVerified: boolean = false;
    public rememberMe: boolean = false;

}
