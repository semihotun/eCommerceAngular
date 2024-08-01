import { CommonModule } from '@angular/common';
import { Component, inject, Input, OnInit } from '@angular/core';
import { CategoryTreeDTO } from 'src/app/models/responseModel/category';
import { IonIcon, ModalController } from '@ionic/angular/standalone';
import { CreateOrUpdateCategoryPage } from '../../../create-or-update-category/create-or-update-category.page';
import { CategoryService } from 'src/app/services/category/category.service';
@Component({
  selector: 'app-category-tree',
  templateUrl: './category-tree.component.html',
  styleUrls: ['./category-tree.component.scss'],
  standalone: true,
  imports: [CommonModule, IonIcon],
})
export class CategoryTreeComponent implements OnInit {
  @Input() categories: CategoryTreeDTO[] = [];
  @Input() level: number = 0;
  maxLevel: number = 1;
  modalController = inject(ModalController);
  categoryService = inject(CategoryService);
  constructor() {}

  ngOnInit() {}
  async openUpdateModal(parentCategoryId: string) {
    const modal = await this.modalController.create({
      component: CreateOrUpdateCategoryPage,
      componentProps: {
        parentCategoryId: parentCategoryId,
      },
    });
    modal.present();
  }
  async removeCategory(id: string) {
    await this.categoryService.deleteCategory(id);
    await this.categoryService.getCategoryTree();
  }
  async editCategory(id: string) {
    const modal = await this.modalController.create({
      component: CreateOrUpdateCategoryPage,
      componentProps: {
        Id: id,
      },
    });
    modal.present();
  }
}
