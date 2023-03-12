import { Pipe, PipeTransform } from '@angular/core';

@Pipe( { name: 'phone' } )
export class PhoneNumberPipe implements PipeTransform {
    
    transform( phoneNumber: string ): string {

        return `(${phoneNumber.slice( 0, 3 )})${phoneNumber.slice( 3, 6 )}-${phoneNumber.slice( 6 )}`;

    }
}
