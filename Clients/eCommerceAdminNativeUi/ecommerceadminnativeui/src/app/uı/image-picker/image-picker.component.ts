import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  forwardRef,
  Input,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Camera, CameraResultType, CameraSource } from '@capacitor/camera';
import { Platform, IonButton } from '@ionic/angular/standalone';

@Component({
  selector: 'app-image-picker',
  templateUrl: './image-picker.component.html',
  styleUrls: ['./image-picker.component.scss'],
  standalone: true,
  imports: [IonButton, CommonModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ImagePickerComponent),
      multi: true,
    },
  ],
})
export class ImagePickerComponent implements OnInit, ControlValueAccessor {
  public disabled: boolean = false;
  @Input() placeholder: string = '';
  @Input() label: string = '';
  onChange: any = () => {};
  onTouched: any = () => {};
  value: string | null = null;
  selectedImage: string = '../../../assets/images/default.png';
  isWeb: boolean;
  @ViewChild('fileInput', { static: false })
  fileInput!: ElementRef<HTMLInputElement>;

  constructor(private platform: Platform) {
    this.isWeb = !this.platform.is('hybrid');
  }

  ngOnInit(): void {}

  async selectImageFromGallery() {
    if (this.isWeb) {
      this.fileInput.nativeElement.click();
    } else {
      const image = await Camera.getPhoto({
        quality: 90,
        allowEditing: false,
        resultType: CameraResultType.Base64,
        source: CameraSource.Photos,
      });
      this.selectedImage = `data:image/jpeg;base64,${image.base64String}`;
      this.valuChange();
    }
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        const base64String = (reader.result as string).split(',')[1];
        this.selectedImage = `data:image/jpeg;base64,${base64String}`;
        this.valuChange();
      };
      reader.readAsDataURL(file);
    }
  }
  valuChange() {
    this.value = this.selectedImage;
    this.onChange(this.value);
    this.onTouched();
  }

  writeValue(obj: any): void {
    if (obj !== undefined && this.value !== obj && obj != '') {
      //daha sonra değişicek
      this.selectedImage = obj;
      this.value = obj;
      this.onChange(this.value);
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
}
