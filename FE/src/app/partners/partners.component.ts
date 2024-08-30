import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-partners',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './partners.component.html',
  styleUrl: './partners.component.css'
})
export class PartnersComponent {
  partners = [
    { name: 'Aruna Vietnam', logo: 'Aruna.jpg' },
    { name: 'Bantique Hotel', logo: 'bantique.png' },
    { name: 'Best Express', logo: 'best-express.png' },
    { name: 'Dr. Bull Spa', logo: 'dr-bull-spa.png' },
    { name: 'CharityLife', logo: 'image.png' },
    { name: 'Linh Nguyen Event', logo: 'linh-nguyen.png' },
    { name: 'Mango Trip', logo: 'assets/images/mango-trip.png' },
    { name: 'Nhựa Bảo Minh', logo: 'assets/images/nhua-bao-minh.png' },
    { name: 'Thành Phát Stone', logo: 'assets/images/thanh-phat-stone.png' },
    { name: 'Interior Decoration', logo: 'assets/images/interior-decoration.png' },
    { name: 'Vbook Shop', logo: 'assets/images/vbook-shop.png' },
    { name: 'Hà Nội Chính Hãng', logo: 'assets/images/ha-noi-chinh-hang.png' }
  ];
}
