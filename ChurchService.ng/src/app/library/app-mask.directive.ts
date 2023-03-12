import { Directive, HostListener, ElementRef, Input } from '@angular/core';
import { NgControl, NgModel } from '@angular/forms';
import { StringUtilities } from '../library/utilities';

export interface InputEvent {
    data: string
    target: HTMLInputElement
    inputType: InputTypes
    which: number
}

export interface KeyboardEvent {
    key: string
    target: HTMLInputElement
    preventDefault: () => null
    which: number
    ctrlKey: boolean
    altKey: boolean
}

export enum InputTypes {
    insertText = 'insertText',
    deleteContentBackward = 'deleteContentBackward',
    deleteContentForward = 'deleteContentForward'
}

@Directive( { selector: '[appMask]' } )
export class AppMaskDirective {

    private readonly keyWords: string[] = [ '#', '$', '*' ];
    private readonly alwaysValidKeys: number[] = [ 8, 9, 13, 16, 17, 18, 19, 20, 27, 33, 34, 35, 36, 37, 38, 39, 40, 45, 46, 91, 92, 93, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 144, 145 ];
    private inputPosition: number = 0;

    @Input() public appMask: string;

    @HostListener( 'input', [ '$event' ] ) onModelChange( e: InputEvent ): void {
        this.onInputChange( e );
    }

    @HostListener( 'keydown', [ '$event' ] ) onKeydown( e: KeyboardEvent ): void {

        if ( !e.altKey && !e.ctrlKey ) {
            if ( StringUtilities.isNullOrWhitespace( e.target.value ) ) {
                // if the input is in a pristine state, then we need to evaluate the first enterable value given.
                for ( let i: number = 0; i < this.appMask.length; i++ ) {
                    if ( this.keyWords.includes( this.appMask.charAt( i ) ) ) {
                        if ( !this.isValidInput( e.which, i ) ) {
                            e.preventDefault();
                        }
                        break;
                    }
                }
            } else {
                if ( !e.altKey && !e.ctrlKey && !this.isValidInput( e.which, e.target.selectionStart != null ? e.target.selectionStart : -1 ) ) {
                    e.preventDefault();
                }

            }
        }

        this.inputPosition = e.target.selectionStart != null ? e.target.selectionStart : -1;
    }

    constructor( private readonly ngControl: NgControl, private readonly el: ElementRef<HTMLInputElement>, private readonly ngModel: NgModel ) {
    }





    /**
     * When the input's value has changed, recalculate the mask.
     * 
     * @param event The input event arguments.
     */
    public onInputChange( event: InputEvent ): void {
        let clean: string = this.getCleanValue( event.target.value );
        let masked: string = this.getMaskedValue( clean );

        this.ngModel.update.emit( masked );

        this.inputPosition = event.target.selectionStart != null ? event.target.selectionStart : -1;

        if ( masked.length - 1 !== this.inputPosition ) {
            
            window.setTimeout( () => {
                let i: number = this.getNextInputPosition( this.inputPosition );

                event.target.selectionStart = i;
                event.target.selectionEnd = i;
            } );
            
        }
        
    }



    /**
     * Returns true if the given which code is a possible value for the current
     * input position.
     * 
     * @param which The which value of the keyboard event.
     * @param maskIndex The current mask index.
     */
    private isValidInput( which: number, maskIndex: number ): boolean {
        if ( !this.alwaysValidKeys.includes( which ) ) {
            if ( this.appMask.charAt( maskIndex ) === '#' ) {
                return ( which >= 48 && which <= 57 ) || ( which >= 96 && which <= 105 );
            } else if ( this.appMask.charAt( maskIndex ) === '$' ) {
                return which >= 65 && which <= 90;
            } else if ( this.appMask.charAt( maskIndex ) === '*' ) {
                return ( which >= 48 && which <= 57 ) || ( which >= 96 && which <= 105 ) || ( which >= 65 && which <= 90 );
            } else {
                return false;
            }
        } else {
            return true;
        }
    }



    /**
     * Returns true if the given character code is a possible value for the 
     * current input position.
     * 
     * @param charCode The character code to evaluate.
     * @param maskIndex The current mask index.
     */
    private isValidCharacterCode( charCode: number, maskIndex: number ): boolean {
        if ( this.appMask.charAt( maskIndex ) === '#' ) {
            return ( charCode >= 48 && charCode <= 57 );
        } else if ( this.appMask.charAt( maskIndex ) === '$' ) {
            return ( charCode >= 65 && charCode <= 90 ) || ( charCode >= 97 && charCode <= 122 );
        } else if ( this.appMask.charAt( maskIndex ) === '*' ) {
            return ( charCode >= 48 && charCode <= 57 ) || ( charCode >= 65 && charCode <= 90 ) || ( charCode >= 97 && charCode <= 122 );
        } else {
            return false;
        }
    }





    /**
     * Returns the real, unmasked value from the given data.
     * 
     * @param v The value to process.
     */
    private getCleanValue( value: string ): string {
        let result: string = '';

        if ( !StringUtilities.isNullOrWhitespace( value ) ) {
            for ( let i: number = 0; i < value.length; i++ ) {
                let code: number = value.charCodeAt( i );
                if ( ( code >= 48 && code <= 57 ) || ( code >= 96 && code <= 105 ) || ( code >= 65 && code <= 90 ) ) {
                    result += value.charAt( i );
                }
            }
        }

        return result;
    }





    /**
     * Returns the masked value based on the given clean data.
     * 
     * @param v The cleaned text value.
     */
    private getMaskedValue( v: string ): string {

        if ( !StringUtilities.isNullOrWhitespace( v ) ) {

            let result: string = '';
            let index: number = 0;

            for ( let i: number = 0; i < this.appMask.length; i++ ) {
                if ( this.keyWords.includes( this.appMask.charAt( i ) ) ) {
                    if ( index >= v.length ) {
                        break;
                    }

                    while ( index < v.length ) {
                        if ( this.isValidCharacterCode( v.charCodeAt( index ), i ) ) {
                            result += v.charAt( index );
                            index++;
                            break;
                        }
                        index++;
                    }
                    
                } else {
                    result += this.appMask.charAt( i );
                }
            }

            return result;
        } else {

            return v;
        }

    }

    /**
     * Returns the next index position for user input.
     * 
     * ex.
     *  Given the mask: ###-###
     *  The user enters the numeral 4 at position: 11|1-1
     *  The result should put the cursor at the next enterable value: 114-|11
     *  The user enters a value in index 2, because index 3 is the hyphen, this method will return the value 4.
     * 
     * @param index The current cursor index.
     */
    private getNextInputPosition( index: number ): number {
        for ( let i: number = index; i < this.appMask.length; i++ ) {
            if ( this.keyWords.includes( this.appMask.charAt( i ) ) ) {
                return i;
            }
        }
        return this.appMask.length;
    }

}
