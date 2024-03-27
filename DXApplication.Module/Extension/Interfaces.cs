using System;
using System.Linq;

namespace DXApplication.Module.Extension;

/// <summary>
/// Chuyển root list view thành dạng inline (không mở detail view)
/// </summary>
public interface IListViewInline { }

/// <summary>
/// Chuyển nested list view thành dạng inline (không mở detail view)
/// </summary>
public interface INestedListViewInline { }

/// <summary>
/// Chuyển root list view thành dạng popup (mở detail view trên popup)
/// </summary>
public interface IListViewPopup { }

/// <summary>
/// Ẩn các controller không cần thiết trên domain component
/// </summary>
public interface IDomainComponent { }
/// <summary>
/// Refresh menu navigation sau khi object mới được tạo
/// </summary>
public interface IRefreshNavigation { }
/// <summary>
/// Đếm số lượng collection trong detailview
/// </summary>
public interface IDetailViewCount { }
