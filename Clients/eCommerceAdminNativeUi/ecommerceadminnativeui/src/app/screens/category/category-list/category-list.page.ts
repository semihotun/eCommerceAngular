import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  IonContent,
  IonHeader,
  IonTitle,
  IonToolbar,
  IonIcon,
  ModalController,
} from '@ionic/angular/standalone';
import { HeaderComponent } from 'src/app/uı/header/header.component';
import { CategoryService } from 'src/app/services/category/category.service';
import { CategoryStore } from 'src/app/stores/category.store';
import { CategoryTreeComponent } from './components/category-tree/category-tree.component';
import { CreateOrUpdateCategoryPage } from '../create-or-update-category/create-or-update-category.page';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.page.html',
  styleUrls: ['./category-list.page.scss'],
  standalone: true,
  imports: [
    IonIcon,
    IonContent,
    IonHeader,
    IonTitle,
    IonToolbar,
    CommonModule,
    FormsModule,
    HeaderComponent,
    CategoryTreeComponent,
  ],
})
export class CategoryListPage implements OnInit {
  constructor(private modalController: ModalController) {}

  async ngOnInit() {
    this.getCategoryTree();
  }
  categoryStore = inject(CategoryStore);
  categoryService = inject(CategoryService);
  async getCategoryTree() {
    await this.categoryService.getCategoryTree();
  }
  async openCreateModal() {
    const modal = await this.modalController.create({
      component: CreateOrUpdateCategoryPage,
    });
    modal.present();
  }
}
