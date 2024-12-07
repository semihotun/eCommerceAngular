import { Directive, ElementRef, Renderer2, HostListener } from '@angular/core';

@Directive({
  selector: 'input',
  standalone: true,
})
export class FocusScrollDirective {
  constructor(private el: ElementRef, private renderer: Renderer2) {}

  @HostListener('focus')
  onFocus() {
    this.scrollToElement();
  }

  private scrollToElement() {
    const element = this.el.nativeElement;
    this.el.nativeElement.scrollIntoView({
      behavior: 'smooth',
      block: 'center',
      //En tepeye gelmesi için
      // top: element.getBoundingClientRect().top + window.pageYOffset,
      // behavior: 'smooth',
    });
  }
}
