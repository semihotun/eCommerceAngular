export function formData(data: any) {
  const form = new FormData();
  for (let prop of Object.keys(data)) {
    form.append(prop, data[prop]);
  }
  return form;
}
