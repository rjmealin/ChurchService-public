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

@Directive( { selector: '[phoneMask]' } )
export class PhoneMaskDirective {

    @Input() public phoneMask: string;

    @HostListener( 'input', [ '$event' ] ) onModelChange( e: InputEvent ): void {
        
        let newVal = e.target.value;

        let nums = newVal.replace('[^0-9]','');

        if(nums.length < 4){
            nums = nums.replace(/^(\d{0,3})/, '($1)');
        } else if (nums.length < 7){
            nums = nums.replace(/^(\d{0,3})(\d{0,3})/, '($1) $2');
        } else {
            nums = nums.replace(/^(\d{0,3})(\d{0,3})(.*)/, '($1) ($2)-$3');
        }

        //this.ngModel.valueAccessor?.writeValue(nums);
        this.el.nativeElement.value = nums;
    }

    @HostListener( 'keydown', [ '$event' ] ) onKeydown( e: KeyboardEvent ): void {

        if((e.which < 96 || e.which > 105) && (e.which < 48 || e.which > 57) && e.which !== 8 && e.which !== 9){
            e.preventDefault();
        }
        
    }

    constructor( private readonly ngControl: NgControl, private readonly el: ElementRef<HTMLInputElement>, private readonly ngModel: NgModel ) {
    }


}