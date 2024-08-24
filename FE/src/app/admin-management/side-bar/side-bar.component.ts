import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatNavList } from '@angular/material/list';
import { MatSidenav, MatSidenavContainer, MatSidenavContent } from '@angular/material/sidenav';
import { MatToolbar } from '@angular/material/toolbar';

@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [MatSidenav, MatIcon, MatNavList, MatSidenavContainer, MatSidenavContent, MatToolbar],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css'
})
export class SideBarComponent {
}
