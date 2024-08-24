import { Component, Input } from '@angular/core';


declare var bootstrap: any;
@Component({
  selector: 'app-card-detail',
  standalone: true,
  imports: [],
  templateUrl: './card-detail.component.html',
  styleUrl: './card-detail.component.css'
})
export class CardDetailComponent {
  @Input() card: any;
  @Input() modalId!: string;
}
