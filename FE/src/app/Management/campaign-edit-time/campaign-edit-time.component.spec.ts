import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignEditTimeComponent } from './campaign-edit-time.component';

describe('CampaignEditTimeComponent', () => {
  let component: CampaignEditTimeComponent;
  let fixture: ComponentFixture<CampaignEditTimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CampaignEditTimeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CampaignEditTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
