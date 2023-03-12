import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CalendarEvent } from 'angular-calendar';
import { Observable } from 'rxjs';
import { AddMemberModel } from '../church/church.model';
import { EmailBlastModel, VisitorCardModel } from '../users/users.model';
import { AddMemberComponent } from './add-member/add-member.component';
import { AddVisitorCardComponent } from './add-visitor-card/add-visitor-card.component';
import { AlertDialogComponent } from './alert/alert.component';
import { AssignMemberComponent } from './assign-member/assign-member.component';
import { ConfirmComponent } from './confirm/confirm.component';
import { EmailBlastComponent } from './email-blast/email-blast.component';
import { VisitorCardDetailsComponent } from './visitor-card-details/visitor-card-details.component';

@Injectable( { providedIn: 'root' } )
export class DialogService {
    
    constructor( private readonly dialog: MatDialog ) { }
    

    /**
     * Opens a simple alert dialog with a title and message.
     * 
     * @param title The title for the error dialog.
     * @param messages The message(s) to display to the user.
     * @param onClose If given, this function is executed after the dialog is closed.
     */
    public showAlertDialog( title: string, messages: string | string[], showError: boolean, showWarning: boolean, onClose?: () => void ): void {
            
        let dialogRef: MatDialogRef<AlertDialogComponent> = this.dialog.open( AlertDialogComponent, {
            width: '450px',
            data: {
                title,
                message: messages,
                showError,
                showWarning,
            },
        } );
    
        if ( onClose != null ) {
            dialogRef.afterClosed().subscribe( onClose );
        }
    }










    /**
     * Shows a dialog to allow to user to save a visitor card
     */
    public ShowVisitorCardDialog(): Observable<VisitorCardModel | undefined>{
        return this.dialog.open<AddVisitorCardComponent, any, VisitorCardModel>( AddVisitorCardComponent, {
            width:'600px',
            height:'700px'
        }).afterClosed();
    }









    /**
     * Shows a dialog to allow to user to add a church member
     */
    public ShowAddMemberDialog(): Observable<AddMemberModel | undefined>{
        return this.dialog.open<AddMemberComponent, any, AddMemberModel>( AddMemberComponent, {
            width:'600px',
            height:'700px'
        }).afterClosed();
    }











    /**
     *  Shows a confirmation dialog to user user 
     */
    public ShowConfirmDialog(message:string, title:string): Observable<boolean | undefined> {
        return this.dialog.open<ConfirmComponent, any, boolean>(ConfirmComponent, {
            width:'400px',
            height:'200px',
            data: {Title: title, Message:message}
        }).afterClosed();
    }









    
    /**
     *  Shows a dialog to assign a member to a planned visit 
     */
    public ShowAssignMemberDialog(event:CalendarEvent): Observable<boolean | undefined>{
        return this.dialog.open<AssignMemberComponent, any, boolean>(AssignMemberComponent, {
            width:'600px',
            minHeight:'300px',
            data: event
        }).afterClosed();
    }









    
    /**
     *  Shows a dialog to assign a member to a planned visit 
     */
    public ShowVisitorCardDetailsDialog(card:VisitorCardModel): Observable<boolean | undefined>{
        return this.dialog.open<VisitorCardDetailsComponent, any, boolean>(VisitorCardDetailsComponent, {
            width:'900px',
            minHeight:'300px',
            data: card
        }).afterClosed();
    }









    
    /**
     * Shows the dialog to write and send an email blast
     */
    public ShowEmailBlastDialog(): Observable<EmailBlastModel | undefined> {
        return this.dialog.open<EmailBlastComponent, any, EmailBlastModel>(EmailBlastComponent, {
            width:'600px',
            height:'700px'
        }).afterClosed();
    }
}
