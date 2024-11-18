FROM node:22.0.0 AS nodebuilder
WORKDIR /usr/src/app

# package.json ve diğer dosyaları kopyala
COPY Clients/eCommerceAdminNativeUi/ecommerceadminnativeui/package.json ./
COPY Clients/eCommerceAdminNativeUi/ecommerceadminnativeui/package-lock.json ./

# Bağımlılıkları yükle
RUN npm install

# Tüm kaynak dosyalarını kopyala
COPY Clients/eCommerceAdminNativeUi/ecommerceadminnativeui/ ./

# Ionic build işlemini çalıştır
RUN npm run build -- --configuration production


FROM nginx:alpine
# Ionic build çıktısını kopyala
COPY --from=nodebuilder /usr/src/app/www /usr/share/nginx/html
COPY Clients/eCommerceAdminNativeUi/ecommerceadminnativeui/nginx.conf /etc/nginx/nginx.conf
