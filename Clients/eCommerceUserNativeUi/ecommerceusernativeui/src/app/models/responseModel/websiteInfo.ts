export class WebsiteInfo {
  id: string = '6d4b21fb-70a3-4618-86a2-9ee652646595';
  socialMediaText!: string;
  logo!: string;
  webSiteName!: string;
  socialMediaInfos: SocialMediaInfo[] = [];
}

export class SocialMediaInfo {
  id!: string;
  platformName!: string;
  url!: string;
  icon!: string;
}
