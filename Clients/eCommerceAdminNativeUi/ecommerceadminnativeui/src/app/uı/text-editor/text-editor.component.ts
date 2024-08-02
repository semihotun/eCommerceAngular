import { CommonModule } from '@angular/common';
import { Component, forwardRef, Input, OnInit } from '@angular/core';
import {
  FormsModule,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule,
  ControlValueAccessor,
} from '@angular/forms';
import { QuillModule } from 'ngx-quill';

@Component({
  selector: 'app-text-editor',
  templateUrl: './text-editor.component.html',
  styleUrls: ['./text-editor.component.scss'],
  standalone: true,
  imports: [QuillModule, ReactiveFormsModule, FormsModule, CommonModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TextEditorComponent),
      multi: true,
    },
  ],
})
export class TextEditorComponent implements OnInit, ControlValueAccessor {
  editorContent = '';
  editorConfig = {
    toolbar: [
      ['bold', 'italic', 'underline', 'strike'],
      ['blockquote', 'code-block'],
      [{ header: 1 }, { header: 2 }],
      [{ list: 'ordered' }, { list: 'bullet' }],
      [{ script: 'sub' }, { script: 'super' }],
      [{ indent: '-1' }, { indent: '+1' }],
      [{ direction: 'rtl' }],
      [{ size: ['small', false, 'large', 'huge'] }],
      [{ header: [1, 2, 3, 4, 5, 6, false] }],
      [{ color: [] }, { background: [] }],
      [{ font: [] }],
      [{ align: [] }],
      ['clean'],
      ['link', 'image', 'video'],
    ],
  };

  @Input() label: string = '';
  @Input() class: string = '';
  onChange: any = () => {};
  onTouched: any = () => {};
  public disabled: boolean = false;

  constructor() {}

  ngOnInit(): void {}

  writeValue(obj: any): void {
    if (obj !== undefined && this.editorContent !== obj) {
      this.editorContent = obj;
      this.onChange(this.editorContent);
      this.onTouched();
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  onContentChanged(event: any): void {
    this.editorContent = event.html;
    this.onChange(this.editorContent);
    this.onTouched();
  }
}
