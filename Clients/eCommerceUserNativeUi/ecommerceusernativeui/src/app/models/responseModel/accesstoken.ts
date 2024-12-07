export class AccessToken {
  claims!: ClaimTypeValue[];
  token!: string;
  expiration!: string;
}
export class ClaimTypeValue {
  claimType!: string;
  value!: string;
}
