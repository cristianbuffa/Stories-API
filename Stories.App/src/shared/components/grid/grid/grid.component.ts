import { Component, Input, OnDestroy } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ColDef, GridApi, ICellRendererParams, GridReadyEvent } from 'ag-grid-community';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnDestroy {
  
  private gridApi!: GridApi;
  filterControl = new FormControl('');
  
  formControlSubscription: Subscription;
  @Input() gridData: any | null | undefined;


  constructor(){    

    this.formControlSubscription = this.filterControl.valueChanges.subscribe(value => {
      this.gridApi.setGridOption("quickFilterText", value?.toString());
    }
    );
  }

  // Column Definitions: Defines & controls grid columns.
  colDefs: ColDef[] = [
    {
      field: 'title',
      cellRenderer: function (params: ICellRendererParams) {
        if (params.data.url)
          return `<a href="${params.data.url}" target="_blank">` + params.data.title + '</a>';
        else
          return params.data.title;
      }
    }
  ];

  defaultColDef: ColDef = {
    flex: 1,
  }

  onGridReady(params: GridReadyEvent) {
    this.gridApi = params.api;
  }

  onRowDoubleClick() {
    alert('UnAuthorize User');
  }

  ngOnDestroy(): void {
    this.formControlSubscription.unsubscribe();
  }
}
