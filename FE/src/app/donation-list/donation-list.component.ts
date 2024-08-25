import { Component, OnInit } from '@angular/core';
import { DonationCardComponent } from "../donation-card/donation-card.component";
import { CommonModule } from '@angular/common';

interface Card {
  cardId: number;
  cardImage: string;
  cardTitle: string;
  cardPartner: string;
  cardDayLeft: number;
  currentAmount: number;
  targetAmount: number;
  contributionCount: number;
  achievedPercentage: number;
  cardDetail: string;
  campaignCode: number
}

@Component({
  selector: 'app-donation-list',
  standalone: true,
  imports: [DonationCardComponent, CommonModule],
  templateUrl: './donation-list.component.html',
  styleUrl: './donation-list.component.css'
})
export class DonationListComponent implements OnInit {
  cards: Card[] = [
    {
      cardId: 1,
      cardImage: 'card-img-0.jpg',
      cardTitle: 'Gây quỹ giúp người dân thôn Dền Thàng, xã Tả Van, huyện Sa Pa, tỉnh Lào Cai có cầu dân sinh kiên cố và ...',
      cardPartner: 'Sức mạnh 2000',
      cardDayLeft: 64,
      currentAmount: 190000042,
      targetAmount: 200000000,
      contributionCount: 1685,
      achievedPercentage: 0,
      cardDetail: 'Từ thị trấn Sa Pa, xuôi theo con đường mòn uốn lượn lưng chừng núi khoảng 8km về phía nam, xã Tả Van nằm trong thung lũng Mường Hoa, huyện Sa Pa, tỉnh Lào Cai - nơi có hai bãi đá cổ nổi tiếng và con suối Mường Hoa chảy quaTả Van là xã khó khăn của huyện Sa Pa, xã có 7 bản gồm: Tả Van Giáy 1, Tả Van Giáy 2, Tả Chải Mông, Tả Van Mông, Tả Chải Dao, Séo Mý Tỷ và Dền Thàng. Dền Thàng là bản người Mông nằm sâu nhất trong Vườn quốc gia Hoàng Liên. Người Mông ở đây dùng đất làm nhà trình tường ngăn giá lạnh, che gió sương. Họ khai hoang ruộng bậc thang từ những mạch nước nhỏ ngang sườn núi, dưới thung sâu. Khí hậu rét lạnh nên chỉ cấy được một vụ. Vào mùa giáp hạt họ nhờ cây tam giác mạch mà vượt qua cái đói, rồi cứ thế bám đất, bám rừng lập bản.Đất ở vùng Tả Van khô cằn chủ yếu là đất dốc nên người dân chỉ trồng ngô, sắn năng suất thấp, hiệu quả kinh tế không cao. Do vậy, đời sống của họ vẫn còn rất khó khăn. Đặc biệt hiện nay vẫn giao thông đi đến thôn Dền Thàng - khu vực vùng lõi của rừng Hoàng Liên. Và có lẽ đây cũng chính là một trong những nguyên nhân chính cản trở sự phát triển kinh tế địa phương. Một trong số những khó khăn về giao thông phải kể đến cầu thôn Dền Thàng, nơi giao thương chính của các hộ dân nơi đây.',
      campaignCode: 254773
    },
    {
      cardId: 2,
      cardImage: 'card-img-1.jpg',
      cardTitle: 'Chung tay trao tặng 30 phần quà gồm học bổng và xe đạp dành tặng cho các em học sinh nghèo hiếu học...',
      cardPartner: 'Phòng chống thiên tai',
      cardDayLeft: 64,
      currentAmount: 4903789,
      targetAmount: 150000000,
      contributionCount: 839,
      achievedPercentage: 0,
      cardDetail: 'Nghệ An, Hà Tĩnh là 2 tỉnh thuộc khu vực miền Trung, một trong những khu vực thường phải hứng chịu nhiều thảm họa thiên tai nhất cả nước. Đó cũng là nơi đón những cơn bão đổ bộ vào từ hướng Đông, sạt lở và lũ quét ở hướng Tây một cách dồn dập. Thời tiết khắc nghiệt vô cùng, mùa nắng đến thì cháy da thịt, mùa mưa về thì tối tăm mặt mũi do eo đất miền trung khá hẹp, bên Đông là đồng bằng giáp biển còn sườn Tây là những dãy núi cao trập trùng dẫn đến địa hình chia cắt mạnh theo hướng Tây sang Đông. Hơn thế, Nghệ An và Hà Tĩnh còn là một trong những chiến trường vô cùng ác liệt thời kỳ kháng chiến chống Pháp và chống Mỹ cứu nước. Do khu vực đồi núi phía Tây là nơi sinh sống của nhiều đồng bào dân tộc thiểu số, đường xá giao thông đi lại khó khăn, dân cư thưa thớt, nhiều khu vực còn canh tác lạc hậu, cô lập với bên ngoài nên kinh tế kém phát triển. Tuy khó khăn và vất vả nhưng những em nhỏ ở nơi đây vẫn luôn cố gắng phấn đấu học tập không ngừng, bởi các em hiểu rõ rằng học tập, trao rồi kiến thức, nâng cao trình độ tri thức là chìa khóa duy nhất dẫn tới sự thành công cho bản thân của các em. Thế nhưng, nhiều em học sinh tại đây vẫn còn gặp phải vô vàn hoàn cảnh khó khăn đứng trước nguy cơ phải bỏ học giữa chừng do điều kiện kinh tế không cho phép, nhà ở cách xa điểm trường. Gánh nặng về kinh tế gia đình luôn là bài toán khó đè nặng lên đôi vai của gia đình các em. Với các em, hoàn cảnh gia đình khó khăn đến mức sinh sống trong những mái nhà tranh tạm bợ, bữa đói bữa no thì việc được đến trường học con chữ càng khó khăn hơn bao giờ hết. Chính vì thế mà nhiều em đành gác lại giấc mơ của bản thân để phụ giúp gia đình.',
      campaignCode: 412321
    },
    {
      cardId: 3,
      cardImage: 'card-img-2.jpg',
      cardTitle: 'Gây quỹ triển khai dự án khám & tư vấn sức khỏe, cấp thuốc miễn phí cho người dân khó khăn, người dâ...',
      cardPartner: 'Quỹ từ thiện Hoa Chia Sẻ',
      cardDayLeft: 64,
      currentAmount: 12688643,
      targetAmount: 150000000,
      contributionCount: 1080,
      achievedPercentage: 0,
      cardDetail: 'Chi tiết quyên góp 1...',
      campaignCode: 313454
    },
    {
      cardId: 4,
      cardImage: 'card-img-0.jpg',
      cardTitle: 'Gây quỹ triển khai dự án khám & tư vấn sức khỏe, cấp thuốc miễn phí cho người dân khó khăn, người dâ...',
      cardPartner: 'Quỹ từ thiện Hoa Chia Sẻ',
      cardDayLeft: 64,
      currentAmount: 12688643,
      targetAmount: 150000000,
      contributionCount: 1080,
      achievedPercentage: 0,
      cardDetail: 'Chi tiết quyên góp 1...',
      campaignCode: 441023
    }
    // Thêm các thẻ khác nếu cần
  ];
  constructor() { }

  ngOnInit(): void {
    // Tính toán achievedPercentage và làm tròn 2 chữ số thập phân
    this.cards.forEach(card => {
      card.achievedPercentage = parseFloat(((card.currentAmount / card.targetAmount) * 100).toFixed(2));
    });
  }
}
