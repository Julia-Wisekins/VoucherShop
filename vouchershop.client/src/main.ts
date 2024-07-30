import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './view/voucher/voucher.module';

platformBrowserDynamic().bootstrapModule(AppModule, {
  ngZoneEventCoalescing: true
})
  .catch(err => console.error(err));
