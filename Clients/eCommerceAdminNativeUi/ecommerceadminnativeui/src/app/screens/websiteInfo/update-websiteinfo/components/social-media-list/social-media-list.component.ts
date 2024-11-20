import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { SocialMediaInfo } from 'src/app/models/responseModel/websiteInfo';
import { WebSiteInfoService } from 'src/app/services/website-info.service';
import { WebsiteInfoStore } from 'src/app/stores/website-info.store';

@Component({
  selector: 'app-social-media-list',
  templateUrl: './social-media-list.component.html',
  styleUrls: ['./social-media-list.component.scss'],
  standalone: true,
  imports: [CommonModule, TranslateModule],
})
export class SocialMediaListComponent implements OnInit {
  constructor(
    private webSiteInfoService: WebSiteInfoService,
    public websiteInfoStore: WebsiteInfoStore
  ) {}
  selectedImage: string = '../../../../../../assets/images/default.png';
  ngOnInit() {}
  async deleteData(data: SocialMediaInfo) {
    const currentWebsiteInfo = this.websiteInfoStore.websiteInfo$();
    const updatedSocialMediaInfos = currentWebsiteInfo.socialMediaInfos.filter(
      (item) => item.id !== data.id
    );
    const updatedData = {
      ...currentWebsiteInfo,
      socialMediaInfos: updatedSocialMediaInfos,
    };
    await this.webSiteInfoService.updateWebsiteInfo(updatedData);
    await this.webSiteInfoService.getWebsiteInfo();
  }
}
