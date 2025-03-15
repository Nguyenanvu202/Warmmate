import { Component, EventEmitter, inject, Input, input, Output, signal } from '@angular/core';
import { ShopService } from '../../../core/service/shop.service';
import { MatExpansionModule } from '@angular/material/expansion';
import {MatListOption, MatSelectionList, MatSelectionListChange} from '@angular/material/list';
import { FormsModule } from '@angular/forms';
import { ShopParams } from '../../../shared/models/shopParams';
@Component({
  selector: 'app-filtering-panel',
  standalone: true,
  imports: [
    MatExpansionModule,
    MatSelectionList,
    MatListOption,
    FormsModule
  ],
  templateUrl: './filtering-panel.component.html',
  styleUrl: './filtering-panel.component.scss'
})
export class FilteringPanelComponent {
  readonly panelOpenState = signal(false); // Biến để kiểm tra xem panel có mở hay không
  selectedColors: string[] = [];
  selectedSizes: string[] = [];
  shopParams = new ShopParams();
  @Output() selectedOptionsChange = new EventEmitter();
  shopService = inject(ShopService);


  onColorSelectionChange(event: any): void {
    this.selectedColors = event.source.selectedOptions.selected.map((option: MatListOption) => option.value);
    this.emitCombinedOptions();
  }

  onSizeSelectionChange(event: any): void {
    this.selectedSizes = event.source.selectedOptions.selected.map((option: MatListOption) => option.value);
    this.emitCombinedOptions();
  }

  emitCombinedOptions(): void {
    this.shopParams.options = [...this.selectedColors, ...this.selectedSizes];
    this.selectedOptionsChange.emit(this.shopParams);
  }

}

