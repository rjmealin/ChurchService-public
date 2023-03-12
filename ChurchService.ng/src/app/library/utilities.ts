import { ValidatorFn, AbstractControl, FormControl } from '@angular/forms';












export class StringUtilities {

    /**
     * Returns true if the given value ends with the given search term.
     * 
     * @param value The string value to search.
     * @param search The term to search for.
     * 
     */
    public static endsWith( value: string, search: string ): boolean {
        let i: number = value.length - search.length;

        return value.indexOf( search ) === i;
    }










    /**
     * Trims all preceding and trailing whitespace in the given string.  Will
     * also convert all spaces into a single space.
     * 
     * @param value The string value to trim.
     * 
     */
    public static trimAllWhitespace( value: string ): string {
        let val: string = value.toString().trim();

        return val.replace( /\s\s+/g, ' ' );
    }
    









    /**
     * Replaces the character in the given value with the given character at 
     * the given position.
     * 
     * @param value The string value to process.
     * @param char The character to be replaced with.
     * @param index The index the new character will take.
     * 
     */
    public static replaceCharAt( value: string, char: string, index: number ): string {
        let val: string = value;

        if ( index > 0 && index < value.length - 2 ) {
            val = `${value.substr( 0, index )}${char}${value.substr( index + 1 )}`;
        } else if ( index === 0 ) {
            val = `${char}${value.substr( 1 )}`;
        } else if ( index === value.length - 1 ) {
            val = `${value.substr( 0, index )}${char}`;
        }

        return val;
    }
    









    /**
     * Returns true if the given string is all whitespace or null or undefined.
     * 
     * @param value The value to check.
     * 
     */
    public static isNullOrWhitespace( value: string ): boolean {
        return value === undefined || value === null || this.trimAllWhitespace( value ) === '';
    }









    
    /**
     * Capitalizes the given string.
     * 
     * @param value The string to capitalize.
     */
    public static capitalizeFirstLetter( value: string ): string {
        if ( value?.toUpperCase() != null ) {
            return value.charAt( 0 ).toUpperCase() + value.slice( 1 );
        }

        return value;
    }









}
