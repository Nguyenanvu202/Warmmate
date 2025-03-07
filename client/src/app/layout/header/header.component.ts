import { Component } from '@angular/core';
import {MatBadge, MatBadgeModule} from '@angular/material/badge';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatBadge],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

}
