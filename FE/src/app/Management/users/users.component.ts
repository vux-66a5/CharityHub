import { Component, OnInit } from '@angular/core';
import { User } from '../Models/campaign.model';
import { MatDialog } from '@angular/material/dialog';
import { UserViewComponent } from '../user-view/user-view.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserService } from '../user-serivces/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'] // Changed from styleUrl to styleUrls
})
export class UsersComponent implements OnInit {
  users: User[] = []; // Replace with actual data fetching
  filteredUsers: User[] = [];
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalPages: number = 1;

  constructor(public dialog: MatDialog, private userService: UserService, private router: Router) {}

  ngOnInit() {
    // Fetch users data here
    
    this.userService.getViewUsers()
    .subscribe({
      next: (response) => {
        this.users = response;
      }
    })
    this.totalPages = Math.ceil(this.users.length / this.itemsPerPage);
    this.updateFilteredUsers();
  }

  onSearch(emailOrPhone: string) {
    this.userService.searchUsers(emailOrPhone)
    .subscribe({
      next: (response) => {
        this.users = response;
        console.log(this.users)
      }
    })
  }

  // getUsers(): User[] {
  //   // Replace with actual data fetching logic
  //   return [
  //     // Sample data
  //     { 
  //       userId: '1', 
  //       userName: 'john_doe', 
  //       userEmail: 'john.doe@example.com', 
  //       displayName: 'John Doe', 
  //       phoneNumber: '123-456-7890', 
  //       dateCreated: '2023-01-01', 
  //       isActive: true, 
  //       lastLoginDate: '2023-10-01' 
  //     },
  //     { 
  //       userId: '2', 
  //       userName: 'jane_smith', 
  //       userEmail: 'jane.smith@example.com', 
  //       displayName: 'Jane Smith', 
  //       phoneNumber: '098-765-4321', 
  //       dateCreated: '2023-02-15', 
  //       isActive: false, 
  //       lastLoginDate: '2023-09-15' 
  //     },
  //     // More sample data...
  //   ];
  // }

  filterUsers() {
    this.currentPage = 1;
    this.updateFilteredUsers();
  }

  updateFilteredUsers() {
    // const filtered = this.users.filter(user =>
    //   user.email.includes(this.searchTerm) ||
    //   user.phoneNumber.includes(this.searchTerm)
    // );
    // this.totalPages = Math.ceil(filtered.length / this.itemsPerPage);
    // this.filteredUsers = filtered.slice((this.currentPage - 1) * this.itemsPerPage, this.currentPage * this.itemsPerPage);
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updateFilteredUsers();
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updateFilteredUsers();
    }
  }

  viewUser(user: User) {
    this.dialog.open(UserViewComponent, {
      data: user,
      width: '600px',
    });
  }

  toggleUserStatus(user: User, status: boolean) {
    
    this.userService.editActive(user.id, status)
    .subscribe({
      next: (response) => {
        console.log(response)
      }
    })
    window.location.reload();
    // Update the user status in your data source here
  }
}
