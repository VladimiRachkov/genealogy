import { NgModule } from '@angular/core';
import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import {
  faTimes,
  faTrashRestore,
  faSearch,
  faHourglass,
  faCheck,
  faRubleSign,
  faShoppingCart,
  faThumbsUp,
} from '@fortawesome/free-solid-svg-icons';

@NgModule({
  imports: [FontAwesomeModule],
  exports: [FontAwesomeModule],
})
export class AssetsModule {
  constructor(private library: FaIconLibrary) {
    this.library.addIcons(faTimes, faTrashRestore, faSearch, faHourglass, faCheck, faRubleSign, faShoppingCart, faThumbsUp);
  }
}
