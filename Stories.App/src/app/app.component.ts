import { Component, OnDestroy, OnInit } from '@angular/core';
import { ColDef, GridApi, ICellRendererParams, GridReadyEvent } from 'ag-grid-community';
import { StoriesService } from './services/stories.service';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnDestroy {  
  public gridData: any;
  public messages: any;
  storiesSubscription: Subscription;

  constructor(
    private storiesService: StoriesService
  ) {
    this.storiesSubscription = this.storiesService.getStories('priority', 50).subscribe({
      next: this.handleGetStoriesSuccess.bind(this),
      error: this.handleGetStoriesError.bind(this)
    });

  } 

  handleGetStoriesSuccess(data: any) {
    this.gridData = data;
  }


  handleGetStoriesError(error: any) {
    this.messages = error;
  
  }

  ngOnDestroy(): void {
    this.storiesSubscription.unsubscribe();
  }
}
